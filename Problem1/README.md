﻿# Задание 1

_Вы собираетесь совершить долгое путешествие через множество населенных пунктов. Чтобы не запутаться, вы сделали карточки вашего путешествия. Каждая карточка содержит в себе пункт отправления и пункт назначения._

_Гарантируется: если упорядочить эти карточки так, чтобы для каждой карточки в упорядоченном списке пункт назначения на ней совпадал с пунктом отправления в следующей карточке в списке, получится один список карточек без циклов и пропусков._

_Например, у нас есть карточки_

- _Мельбурн &gt; Кельн_
- _Москва &gt; Париж_
- _Кельн &gt; Москва_

_Если упорядочить их в соответствии с требованиями выше, то получится следующий список:_

_Мельбурн &gt; Кельн, Кельн &gt; Москва, Москва &gt; Париж_

_Требуется:_

- _Написать функцию, которая принимает набор неупорядоченных карточек и возвращает набор упорядоченных карточек в соответствии с требованиями выше, то есть в возвращаемом из функции списке карточек для каждой карточки пункт назначения на ней должен совпадать с пунктом отправления на следующей карточке._
- _Дать оценку сложности получившегося алгоритма сортировки_
- _Написать тесты_

_Оценивается прежде всего читабельность кода._

# Комментарии к задаче

После прочтения требований задачи остаётся некоторое количество моментов, которые крайне неоднозначно трактуются.

Первое что хочется уточнить - рассматривать ли названия городов как регистрозависимые или нет? Т.к. не указано иное, по умолчанию сделал их сравнение регистронезависимым (с возможностью замены в дальнейшем).

Также, уже более интересный вопрос к пункту «оценки сложности получившегося алгоритма сортировки». Переведя задачу на более математическое описание, мы получим следующее: для указанного набора элементов требуется сгенерировать их перестановку таким образом, чтобы она (перестановка) удовлетворяла некоторому условию.

Согласен, алгоритм, решающий описанную задачу можно с ходу назвать сортировкой, однако, строгое определение задачи сортировки основывается на наличии между любыми двумя элементами отношения порядка (в простонародье операции сравнения), обладающей свойствами трихотомии [для любых aи b всегда верно ровно одно из трёх утверждений: либо a < b, либо a = b, либо a > b] и транзитивности [a < b, b < c => a < c].
А у меня уже для 4 карточек отношение порядка, удовлетворяющего условию трихотомии, как-то не получается. К примеру, для следующего маршрута между городами:
•	Москва – Ярославль
•	Ярославль – Сергиев-Посад
•	Сергиев-Посад – Мытищи
•	Мытищи – Щёлково
заранее не зная последовательности проезда станций не совсем понятно, как сравнить пару «Москва – Ярославль» и «Мытищи - Щёлково».

Кстати, хотя очевидно, что термин «оценка сложности» скорее всего относится к оценке количества выполняемых операций (т.е. к оценке времени выполнения), он также может относиться и к оценке дополнительной памяти, требуемой для решения задачи.

Далее, помимо асимптотической оценки сложности, на практике крайне важно иметь оценку объёма исходных данных. Для простоты объяснения данного факта (разумеется я в курсе что он Вам известен - просто я привык доказывать свою точку зрения) попробуем выбрать в зависимости от объёма данных N (количества карточек) алгоритм сортировки карточек по городу отправления. Подчеркну, что оценка сугубо априорная; оценивается количество операций сравнения элементов (припусть для примера у нас она есть). Также в следующих чисто теоретических рассуждениях я *намеренно опускаю в оценке сложности алгоритма сложность сравнения элементов*.

1. _Количество карточек не более пары десятков_ – сортировка пузырьком с эвристикой останова (хотя асимптотика решения O(N*N), очень маленькая константа + за счёт последовательного сканирования исходных данных меньше вероятность кэш-промаха).
2. _Количество карточек не более пары десятков тысяч_ – встроенная сортировка C# (начиная с .NET 4 встроенный алгоритм сортировки имеет достаточно приятную структуру чтобы на практике выдержать O(N\*logN) в худшем случае + иметь не сильно высокую константу). Если же имеется требование в независимости от версии .NET, используем сортировку слияниями или quicksort со случайным выбором порогового элемента (в зависимости от того, нужна ли нам O(N\*logN) в худшем случае или достаточно только в среднем).
3. _Количество карточек не более пары десятков миллионов_ – либо остаться на выбранном на предыдущем шаге алгоритме, либо с помощью Dictionary (т.е. хэш-словаря) назначить каждой станции уникальный числовой номер, после чего можно переключиться на цифровую сортировку (radixsort). При таком количестве карточек она опять же за счёт последовательного, а не случайного доступа к памяти может выиграть у «классической» сортировки.
4. _Количество карточек более сотен миллионов и полностью не помещается в отведённой оперативной памяти_ – придётся играться с алгоритмами внешней сортировки (тут мы выбираем внешнюю версию сортировки слияниями или лезем в томик Кнута по сортировке за чем-то более экзотическим). Кстати, та же radixsort вполне подходит и для использования в качестве внешней (проверено).

Это список оценки только по времени – если строить аналогичный исходя их требований минимизации памяти, совпадут, дай Бог, первый и последний пункты.

ОДНАКО все эти варианты имеют место только при решении задачи с использованием классического алгоритма сортировки. Поэтому, поскольку оценивается в первую очередь читабельность кода, написанное мною решение сортировку в явном виде использовать не будет =))

Теперь, когда мы подошли к обсуждению реализованного решения, вспомним, что на практике сравнение двух случайных строк между собой выполняется далеко не за константу; учтём сложность указанный операций в реализованных решениях (хотя бы в среднем).

В результате получем решение *со сложностью по времени O(N\*Avg(L)) в среднем*, где Avg(L) - средняя длина названия города. По-моему получилось достаточно читабельно и несложно.

Далее - см. код решения. 