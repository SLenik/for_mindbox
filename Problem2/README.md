# Задание 2

_Есть таблица хранящая линии покупки: Sales: Id, ProductId, CustomerId, DateCreated. Мы хотим понять, через какие продукты клиенты «попадают» к нам в магазин. Напишите запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента._

# Комментарии к задаче

1. Вообще это интересное упрощение: клиент всегда покупает по одному товару за заказ. Видимо это очень прикольный интернет-магазин, раз про дополнительные продажи он ничего не слышал (хз как это правльно называется, но это когда вместе с телефоном тебе "впендюривают" втридорога чехол, плёнку и расширенную гарантию).

2. Также для более-менее читабельного решения придётся предположить, что один и тот же клиент не имеет нескольких заказов, совершённых в один и тот жемомент времени.

Итак, выведем исходную

```
-- таблицу с данными - для конкретизации плюс в SQL меня приучили к стилю с подчёркиваниями вместо верблюжьего =)
CREATE TABLE Sales (
	id int NOT NULL,
	product_id int NOT NULL,
	customer_id int NOT NULL,
	date_created datetime2 NOT NULL
)
```

# Алгоритм решения задачи

1. Сначала найдём все первые заказы клиентов - обозначим его fo. Если бы для любых двух строк с одним и тем же customer_id нам гарантировали выполнения условия a.date_created < b.date_created <-> a.id < b.id, то искать удобнее было бы по id заказа (как минимум на нём точно должен висеть какой-либо индекс). В принципе, дописывание к описанию таблицы IDENTITY (1, 1) нам бы также гарантировало срабатывание вышеуказанного условия, - но про сие в тексте задаче ничего не сказано =(
2. Далее: каждую запись о покупке товара покупателем джойним с fo. Очевидно, что в результирующем датасете каждый товар повторится столько раз, для скольких клиентов он был первым (такой неявный дистинкт гарантирует нам INNER JOIN).
3. Осталось только сгруппировать по товару и посчитать количество таких записей.

```
SELECT s.product_id,
       count(s.customer_id)
FROM Sales s
INNER JOIN
  ( SELECT customer_id,
           MIN(date_created) AS first_order_date
    FROM Sales
    GROUP BY customer_id
   ) fo
   ON fo.customer_id = s.customer_id
      AND first_order_date = date_created
GROUP BY product_id
```