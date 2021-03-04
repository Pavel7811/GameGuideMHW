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
В предметной области будет 3 БД. Первая БД описывает самих монстров, их виды, свойства, места обитания. Вторая описывает броню, ее типы, свойства.
