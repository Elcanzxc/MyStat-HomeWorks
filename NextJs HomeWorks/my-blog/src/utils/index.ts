


export async function delay(ms:number){
    return new Promise((resolve)=> setTimeout(resolve,ms))
}


export const articles = [
  {
    id: 1,
    title: "Первая статья",
    slug: "first-article",
    description: "Краткое описание первой статьи.",
    content:
      "Это текст-заглушка для первой статьи. Здесь может находиться основной контент, изображения, ссылки и другая информация.",
    author: "Иван Иванов",
    category: "Технологии",
    tags: ["JavaScript", "Frontend"],
    image: "https://picsum.photos/800/400?random=1",
    createdAt: "2026-07-29T10:00:00Z",
  },
  {
    id: 2,
    title: "Вторая статья",
    slug: "second-article",
    description: "Краткое описание второй статьи.",
    content:
      "Это текст-заглушка для второй статьи. Используйте его как пример структуры данных для списка статей.",
    author: "Анна Смирнова",
    category: "Дизайн",
    tags: ["UI", "UX"],
    image: "https://picsum.photos/800/400?random=2",
    createdAt: "2026-07-28T14:30:00Z",
  },
  {
    id: 3,
    title: "Третья статья",
    slug: "third-article",
    description: "Краткое описание третьей статьи.",
    content:
      "Это текст-заглушка для третьей статьи. Позже его можно заменить реальным содержимым.",
    author: "Петр Петров",
    category: "Новости",
    tags: ["Обновления"],
    image: "https://picsum.photos/800/400?random=3",
    createdAt: "2026-07-27T09:15:00Z",
  },
];