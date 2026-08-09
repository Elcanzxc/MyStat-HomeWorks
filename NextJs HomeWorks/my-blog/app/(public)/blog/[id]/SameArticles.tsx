


import { Article, articles, delay } from '@/src/utils';
import Link from 'next/link';
import TagBadge from '@/src/components/TagBadge/TagBadge';


export default async function SameArticles({article}:{article:Article}) {

    

    const sameArticles = articles.filter(
      (item) =>
        item.id !== article.id &&
        item.tags.some((tag) => article.tags.includes(tag))
    );

 await delay(2000)

  return (
    <div className="mt-12 pt-8 border-t border-slate-200 dark:border-slate-800">
      <h2 className="text-2xl font-bold text-slate-800 dark:text-slate-100 mb-4">Похожие по тегам</h2>
       <div className="flex flex-wrap gap-2 mb-6">
         {article.tags.map((item) => (
           <TagBadge key={item} text={item} />
         ))}
       </div>
       
       {sameArticles.length === 0 ? (
         <div className="text-slate-500 dark:text-slate-400 italic bg-slate-50 dark:bg-slate-800/50 p-4 rounded-lg">нет таких</div>
       ) : (
         <div className="grid gap-4 sm:grid-cols-2">
           {sameArticles.map((item) => (
             <div key={item.id} className="group relative bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 rounded-xl p-5 hover:shadow-md hover:border-blue-200 dark:hover:border-slate-500 transition-all"> 
                 <span className="text-xs font-semibold text-slate-500 dark:text-slate-400 border border-slate-200 dark:border-slate-700 px-2.5 py-1 rounded-md mb-2 inline-block">Запись #{item.id}</span>
                 <Link href={`${item.id}`} className="focus:outline-none block mt-2">
                     <span className="absolute inset-0" aria-hidden="true" />
                     <h3 className="text-lg font-bold text-slate-900 dark:text-white group-hover:text-blue-600 dark:group-hover:text-blue-400 transition-colors mb-2 line-clamp-1">{item.title}</h3>
                     <p className="text-sm text-slate-600 dark:text-slate-400 line-clamp-2">{item.content}</p>
                 </Link>
             </div>
           ))}
        </div>
       )}
    </div>
  )
}
