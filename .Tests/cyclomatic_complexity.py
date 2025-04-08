def calculate_cyclomatic_complexity(code):
    """
    Функція для розрахунку цикломатичної складності програмного коду.
    Параметри:
    code (str): Код, який потрібно проаналізувати.

    Повертає:
    int: Цикломатична складність.
    """
    conditions = code.count('if') + code.count('while') + code.count('for')
    edges = code.count('else') + code.count('elif')
    vertices = code.count('{') + code.count('}')
    
    return conditions + edges - vertices + 2  # Формула МакКейба

# Приклад коду
sample_code = """
if x > 10:
    for i in range(5):
        print(i)
else:
    print("x is less than 10")
"""

complexity = calculate_cyclomatic_complexity(sample_code)
print("Цикломатична складність:", complexity)
