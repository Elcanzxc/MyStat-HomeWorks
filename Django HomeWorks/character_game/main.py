class Team:
    def __init__(self, p1, p2):
        self.p1 = p1
        self.p2 = p2

    def __str__(self):
        return f"Команда: {self.p1.name} и {self.p2.name}"


class Character:
    def __init__(self, name, health, attack_power):
        self.name = name
        self.health = health
        self.attack_power = attack_power

    def attack(self, other):
        print(f"{self.name} атакует {other.name}")
        other.take_damage(self.attack_power)

    def take_damage(self, damage):
        self.health -= damage
        print(f"{self.name} получил {damage} урона, осталось здоровья: {self.health}")

    def info(self):
        print(f"Имя: {self.name}, Здоровье: {self.health}, Сила: {self.attack_power}")

    def __str__(self):
        return f"{self.name} (Здоровье: {self.health}, Сила: {self.attack_power})"

    def __add__(self, other):
        return Team(self, other)

    def __lt__(self, other):
        return self.health < other.health

    def __eq__(self, other):
        return self.name == other.name and self.health == other.health

    def __len__(self):
        return self.health

    def __bool__(self):
        return self.health > 0


class Warrior(Character):
    def __init__(self, name, health, attack_power, armor):
        super().__init__(name, health, attack_power)
        self.armor = armor

    def attack(self, other):
        print(f"Воин {self.name} бьет мечом по {other.name}!")
        other.take_damage(self.attack_power + 5)


class Mage(Character):
    def __init__(self, name, health, attack_power, mana):
        super().__init__(name, health, attack_power)
        self.mana = mana

    def attack(self, other):
        print(f"Маг {self.name} пускает фаербол в {other.name}!")
        other.take_damage(self.attack_power + 10)


class Archer(Character):
    def __init__(self, name, health, attack_power, arrows):
        super().__init__(name, health, attack_power)
        self.arrows = arrows

    def attack(self, other):
        print(f"Лучник {self.name} стреляет из лука в {other.name}!")
        other.take_damage(self.attack_power + 3)


warrior = Warrior("Конан", 100, 15, 5)
mage = Mage("Гэндальф", 70, 20, 50)
archer = Archer("Леголас", 80, 12, 10)
enemy = Character("Гоблин", 120, 5)

print(warrior)
print(mage)
print(archer)
warrior.info()

characters = [warrior, mage, archer]
for hero in characters:
    hero.attack(enemy)

my_team = warrior + mage
print(my_team)

print(warrior < mage)
print(warrior == mage)

print(len(archer))

print(bool(enemy))
enemy.take_damage(150)
print(bool(enemy))
