import { articles } from "@/src/utils";
import { notFound } from "next/navigation";
import React from "react";



export default async function Page({ params }: { params: Promise<{ tag: string }> }) {
  const {tag} = await params
  
    const sameArticles = articles.filter((item) =>
    item.tags.some((t) => t.toLowerCase() === tag.toLowerCase())
    );

    if (sameArticles.length === 0) {
    notFound();
    }
  return (
    <div className="space-y-8">
      <div className="flex items-center gap-3 border-b border-slate-200 dark:border-slate-800 pb-4">
        <h2 className="text-2xl font-bold tracking-tight text-slate-800 dark:text-slate-100">Статьи по тегу:</h2>
        <span className="bg-blue-100 dark:bg-blue-900/30 text-blue-700 dark:text-blue-400 px-3 py-1 rounded-full text-sm font-medium">
          #{tag}
        </span>
      </div>

      <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-2">
        {sameArticles.map((item) => (
          <div 
            key={item.id} 
            className="group relative flex flex-col justify-between bg-white dark:bg-slate-800 rounded-2xl p-6 shadow-sm border border-slate-100 dark:border-slate-700 hover:shadow-md hover:border-blue-100 dark:hover:border-slate-600 transition-all duration-200"
          >
            <div>
              <div className="flex items-center justify-between mb-3">
                <span className="text-xs font-semibold text-slate-500 dark:text-slate-400">
                  #{item.id}
                </span>
              </div>
              <h3 className="text-lg font-bold text-slate-900 dark:text-white mb-2 line-clamp-2 group-hover:text-blue-600 dark:group-hover:text-blue-400 transition-colors">
                <a href={`/blog/${item.id}`} className="focus:outline-none">
                  <span className="absolute inset-0" aria-hidden="true" />
                  {item.title}
                </a>
              </h3>
              <p className="text-slate-600 dark:text-slate-300 text-sm line-clamp-3">
                {item.description}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  )

}
