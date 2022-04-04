# DynamicWallpaper
Это проект интерактивных обоев рабочего стола.
Реализованные фичи:
* Вывод температуры
* Вывод даты
* Вывод текущего времени
* Вывод текущего дня недели
---
# Предварительная настройка
1. Настройка в файле Settings/Settings.json параметра Temperature:AppId - id api-ключа на сайте https://openweathermap.org/
2. Настройка шрифта. В репозитории приложен использованный шрифт font.otf, который требует установки.
3. Настройка остальных параметров в Settings/Settings.json по желанию
---
# Описание файла настроек
* WidthScreen - ширина экрана
* HeightScreen - высота экрана
* BackgroundColor - цвет фона
* IndentFromHorizontalEdge - горизонатльный отступ от краев экрана (необходим для Left, Right значений в DynamicX)
* IndentFromVerticalEdge - вертикальный отступ от краев экрана (необходим для Up, Down значений в DynamicY)
* Font - название шрифта
* DayTime - параметры вывода времени и дня недели
* Date - параметры вывода даты
* Temperature - параметры вывода температуры
* CustomStrings - кастомные статичные строки
---
## Общие для DayTime | Date | Temperature | CustomStrings
* IsEnabled - отображать ли данный элемент
* Color - цвет в формате RGB
* Size - размер шрифта
* DynamicX - динамический X (Left, Center, Right, Empty)
* DynamicY - динамический Y (Up, Center, Down, Empty)
* X - позиция по X (учитывается только при заданном DynamicX = Empty)
* Y - позиция по Y (учитывается только при заданном DynamicY = Empty)
---
## Специфичные для Temperature
* City - название города, погогда которого будет отображаться
* AppId - id api-ключа на сайте https://openweathermap.org/
---
## Специфичные для CustomStrings
* Text - текст статичного блока
---
# Пример работы
![image](https://user-images.githubusercontent.com/64701982/161607353-9a1299ab-0d0a-4590-a6b6-6493a5ddf868.png)
