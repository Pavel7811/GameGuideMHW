# GameGuideMHW
# Наша команда:
Гордеев Павел, Щербатых Александр, Кафаров Фарид. Группа ИВТ-365.

Проект создается по игре Monster Hunter World.

Предметной областью является справочник по компьютерной игре Monster Hunter.
В качестве пользователей выступают администратор и игрок.

Функциональные требования заключаются в выведении и добавлении информации о монстрах и броне.
Транзакционные (задачи учёта)
1. Добавить нового монстра;
2. Удалить монстра;
3. Добавить новую броню;
4. Удалить броню.
Справочные (оперативные запросы)
1. Показать монстров, уязвимых к огню.
2. Показать монстров, обитающих в лесу.
3. Показать броню с уровнем редкости А

Первая БД будет описывать монстров.

Сущности и атрибуты:
1. **Монстры**  
  а. Наименование;  
  б. Описание;  
  в. Полезная информация.    

2. **Виды монстров**  
  а. Наименование.    
  
  
3. **Слабые места**  
  а. Наименование.  
  
  
4. **Тип урона**  
  а. Наименование;  
  б. Мощность.  


5. **Повреждаемые части**  
  а. Наименование.  


6. **Элементальные слабости**  
  а. Наименование;  
  б. Мощность.  
  
  
7. **Места обитания**  
  а. Наименование;  
  б. Номер зоны обитания.  
  
**Связи**  
1. Один монстр имеет множество слабостей;  
2. Один монстр находится во множестве мест обитания;  
3. Один монстр имеет множество элементальных слабостей;  
4. Множество повреждаемых частей повреждаются множеством типов урона.  

# ER Диаграмма:  
![ER Диаграмма Монстры](https://github.com/Pavel7811/GameGuideMHW/blob/main/ER%20Diagram%20Monsters.png)  

Вторая БД описывает характеристики брони.

Сущности и атрибуты:
1. **Броня**  
  а. Защита;  
  б. Уровень редкости;  
  в. Цена.    

2. **Сет брони**  
  а. Наименование.    
  
  
3. **Навык**  
  а. Описание;
  б. Количество брони для активации.  
  
  
4. **Материалы для крафта**  
  а. Наименование;  
  б. Количество.  


5. **Умения**  
  а. Наименование; 
  б. Уровень.


6. **Тип брони**  
  а. Наименование.    
  
  
7. **Сопротивляемость стихиям**  
  а. Наименование;  
  б. Мощность.  
  
  
8. **Слоты**  
  а. Уровень слота;  
  б. Количество слотов.  
  
**Связи**  
1. Одна броня имеет множество типов брони;  
2. Множество брони имеет множество умений;  
3. Одна броня имеет множество слотов;    
4. Одна броня имеет один сет брони. 

