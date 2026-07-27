import { delay } from "@/utils/Delay";
import { notFound } from "next/navigation";
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
     <main className="max-w-3xl mx-auto mt-10 bg-white dark:bg-gray-800 p-10 rounded-2xl shadow-lg dark:shadow-gray-900/30">
      <h1 className="text-3xl font-bold mb-6 dark:text-white">Статьи по тегу: {tag}</h1>

      <ul>
        {posts.map((item) => (
          <li key={item.id} className="text-lg leading-relaxed text-gray-700 dark:text-gray-300 mb-2">
            <Link href={`/blog/${item.id}`} className="hover:text-blue-600 dark:hover:text-blue-400 transition-colors">{item.title}</Link>
          </li>
        ))}
      </ul>

      
     </main>
  );
}
