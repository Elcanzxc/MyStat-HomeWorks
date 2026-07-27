import { delay } from "@/utils/Delay";
import { notFound } from "next/navigation";
import styles from "./page.module.css";
import Link from "next/link";

import { blog } from "@/data/blog";


export default async function TagPage({
  params,
}: {
  params: Promise<{ tag: string }>;
}) {
  await delay(2000);

  const { tag } = await params;

const posts = blog.filter((item) => item.tags.includes(tag));

  if (!tag || tag.length === 0) {
    notFound();
  }

  return (
     <main className={styles.container}>
      <h1 className={styles.title}>Статьи по тегу: {tag}</h1>

      <ul>
        {posts.map((item) => (
          <li key={item.id} className={styles.text}>
            <Link href={`/blog/${item.id}`}>{item.title}</Link>
          </li>
        ))}
      </ul>

      
     </main>
  );
}
