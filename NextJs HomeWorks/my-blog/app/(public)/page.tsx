
import LikeArticleButton from "@/src/components/LikeArticleButton";
import { articles } from "@/src/utils";
import Link from "next/link";

export const revalidate = 30;

export default function Home() {
  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between border-b border-slate-200 pb-4">
        <h2 className="text-2xl font-bold tracking-tight text-slate-800">Последние записи</h2>
        <p className="text-sm text-slate-500 bg-slate-100 px-3 py-1 rounded-full">
          Обновлено: {new Date().toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' })}
        </p>
      </div>

      <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {articles.map((item) => (
          <div 
            key={item.id} 
            className="group relative flex flex-col justify-between bg-white rounded-2xl p-6 shadow-sm border border-slate-100 hover:shadow-md hover:border-blue-100 transition-all duration-200"
          >
            <div>
              <div className="flex items-center justify-between mb-3">
                <span className="text-xs font-semibold text-blue-600 bg-blue-50 px-2.5 py-1 rounded-md">
                  #{item.id}
                </span>
              </div>
              <h3 className="text-lg font-bold text-slate-900 mb-2 line-clamp-2 group-hover:text-blue-600 transition-colors">
                <Link href={`blog/${item.id}`} className="focus:outline-none">
                  <span className="absolute inset-0" aria-hidden="true" />
                  {item.title}
                </Link>
              </h3>
              <p className="text-slate-600 text-sm line-clamp-3 mb-4">
                {item.description}
              </p>
            </div>
            
            <div className="relative z-10 pt-4 border-t border-slate-100 mt-auto">
              <LikeArticleButton />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

// Выбрал ISR , ведь блоги могут увеличиватся( в продакшене), SSG не подходит
// Через каждый запрос чекать блоги нет смысла , SSR не подходит




  //   const sameArticles = articles.filter((item) =>
  //   item.tags.some((t) => t.toLowerCase() === tag.toLowerCase())
  //   );
  // console.log(sameArticles)
  //   if (sameArticles.length === 0) {
  //   notFound();
  //   }