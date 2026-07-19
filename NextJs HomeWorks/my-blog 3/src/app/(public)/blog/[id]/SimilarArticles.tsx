import { delay } from "@/utils/Delay";
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




export default async function SimilarArticles({ 
  currentTags, 
  currentId 
}: { 
  currentTags: string[], 
  currentId: number 
}) {

 await delay(2000);
  

 const similar = blog.filter((item) => {
    if(item.id === currentId) return false
    const commonTag = item.tags.some((tag) => currentTags.includes(tag))
    return commonTag;
 })

 
  if (similar.length === 0) {
    return <p>Похожих статей нет.</p>;
  }
    return(
     <ul>
        {similar.map((item)=>(
            <li>
                <Link key={item.id} href={`/blog/${item.id}`}>{item.title}</Link>
            </li>
        ))}
     </ul>
    )
}