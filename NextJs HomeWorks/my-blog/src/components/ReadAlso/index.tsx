import { articles } from "@/src/utils";
import Link from "next/link";


export default async function ReadAlso({ currentId }: { currentId: number }) {
  const otherArticles = articles.filter((a) => a.id !== currentId).slice(0, 2);
  return (
    <div className="grid gap-4 sm:grid-cols-2">
      {otherArticles.map((item) => (
        <div
          key={item.id}
          className="p-4 border rounded-xl bg-white dark:bg-slate-800"
        >
          <span className="text-xs text-blue-600">Запись #{item.id}</span>
          <Link
            href={`/blog/${item.id}`}
            className="block mt-2 font-bold hover:text-blue-500"
          >
            {item.title}
          </Link>
        </div>
      ))}
    </div>
  );
}
