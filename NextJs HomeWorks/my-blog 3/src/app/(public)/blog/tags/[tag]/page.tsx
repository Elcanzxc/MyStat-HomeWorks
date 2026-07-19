import { delay } from "@/utils/Delay";
import { notFound } from "next/navigation";
import styles from "./page.module.css";
import Link from "next/link";

const blog = [
  {
    id: 1,
    title: "Статья №1",
    text: "Сегодня мы расскажем о новых технологиях, которые помогают сделать повседневные задачи проще и эффективнее.",
    tags: ["nextjs", "react"],
  },

  {
    id: 2,
    title: "Статья №2",
    text: "В этом посте делимся полезными советами по развитию навыков, организации времени и поиску вдохновения.",
    tags: ["javascript", "react"],
  },

  {
    id: 3,
    title: "Статья №3",
    text: "Интересные идеи появляются тогда, когда мы экспериментируем и пробуем что-то новое.",
    tags: ["design", "frontend"],
  },
];


export default async function TagPage({
  params,
}: {
  params: Promise<{ tag: string }>;
}) {
  await delay(2000);

  const { tag } = await params;

const posts = blog.filter((item) => {
  item.tags.includes(tag);
});

  if (!tag || tag.length === 0) {
    notFound();
  }

  return (
     <main className={styles.container}>
      <h1 className={styles.title}>Статьи по тегу: {tag}</h1>

      <ul>
        {posts.map((item) => (
          <li className={styles.text}>
            <Link href={`/blog/${item.id}`}>{item.title}</Link>
          </li>
        ))}
      </ul>

      
     </main>
  );
}
