import { articles, delay } from "@/src/utils"
import { notFound } from "next/navigation"

export async function generateStaticParams() {
  return [{id:"1"},{id:"2"},{id:"3"}]
}

export default async function BlogPage({params}:{params:Promise<{id:string}>}) {
  const {id} = await params

  const article = articles.find((item) => item.id === Number(id))
 
  if(article === undefined){
    notFound()
  }
  
  await delay(2000)
  
  return (
    <article className="max-w-2xl mx-auto">
      <header className="mb-8 text-center">
        <span className="text-xs font-semibold text-blue-600 bg-blue-50 px-2.5 py-1 rounded-md mb-4 inline-block">
          Запись #{id}
        </span>
        <h1 className="text-3xl font-extrabold text-slate-900 tracking-tight sm:text-4xl">
          {article.title}
        </h1>
      </header>
      
      <div className="text-lg leading-relaxed text-slate-700 bg-slate-50 p-6 rounded-xl border border-slate-100">
        <p>{article.content}</p>
      </div>
    </article>
  )
}
