import { articles, delay } from "@/src/utils";
import { notFound } from "next/navigation";
import SameArticles from "./SameArticles";
import { Suspense } from "react";
import TagBadge from "@/src/components/TagBadge/TagBadge";
import BlogOnline from "./BlogOnline";
import ShowComments from "@/src/components/ShowComments";

export async function generateStaticParams() {
  return [{ id: "1" }, { id: "2" }, { id: "3" }];
}

export default async function BlogPage({ params,}: {params: Promise<{ id: string }>;}) {
  const { id } = await params;

  const article = articles.find((item) => item.id === Number(id));

  if (article === undefined) {
    notFound();
  }

  await delay(1000);

  return (
    <div>
      <article className="max-w-2xl mx-auto">
        <header className="mb-8 text-center">
          <div>
            <BlogOnline/>
          </div>
          <div className="flex gap-2 justify-center mb-4">
            <span className="text-xs font-semibold text-slate-500 dark:text-slate-400 border border-slate-200 dark:border-slate-700 px-2.5 py-1 rounded-md">
              Запись #{id}
            </span>
            {article.tags.map(tag => <TagBadge key={tag} text={tag} />)}
          </div>
          <h1 className="text-3xl font-extrabold text-slate-900 dark:text-white tracking-tight sm:text-4xl">
            {article.title}
          </h1>
        </header>

        <div className="text-lg leading-relaxed text-slate-700 dark:text-slate-300 bg-slate-50 dark:bg-slate-800/50 p-6 rounded-xl border border-slate-100 dark:border-slate-800">
          <p>{article.content}</p>
        </div>
      </article>
      <div className="mt-8">
        <Suspense fallback={
          <div className="mt-12 pt-8 border-t border-slate-200 dark:border-slate-800">
            <div className="animate-pulse flex space-x-4">
              <div className="flex-1 space-y-4 py-1">
                <div className="h-4 bg-slate-200 dark:bg-slate-800 rounded w-1/4"></div>
                <div className="h-4 bg-slate-200 dark:bg-slate-800 rounded w-1/2"></div>
                <div className="space-y-2">
                  <div className="h-20 bg-slate-200 dark:bg-slate-800 rounded"></div>
                </div>
              </div>
            </div>
            <p className="text-slate-500 dark:text-slate-400 mt-4 text-sm">Ищем похожие по тегам...</p>
          </div>
        }>
          <SameArticles article={article} />
        </Suspense>
             
       <ShowComments/>
      </div>
    </div>
  );
}
