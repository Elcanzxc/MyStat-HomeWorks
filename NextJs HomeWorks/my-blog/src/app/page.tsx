import LastUpdated from "@/components/LastUpdated";
import LikeArticleButton from "@/components/LikeArticleButton.tsx";

export const revalidate = 10;

export default function Home() {
  return (
    <div className="min-h-screen bg-white text-black">
      <main className="max-w-3xl mx-auto p-10">
        <h1 className="text-3xl font-bold mb-6">ISR</h1>

        <p>
         1- потому что количество блогов может увеличиваться или уменьшаться, SSG не подходит 
         2- SSR тоже не подходит потому что при каждом запросе делать обрещение на сервер эффективно, ведь блоги не так быстро появляются
         3- ISR подойдет потому что пусть только при например каждые 30 секунд он обратится на сервер и отрисует новые блоги
          <br/>
          <br/>
        </p>
           <LastUpdated/>

        <div>
          <p>
            Lorem Ipsum is simply dummy text of the printing and typesetting
            industry. Lorem Ipsum has been the industry's standard dummy text
            ever since 1966, when designers at Letraset and James Mosley.
          </p>
          <LikeArticleButton />
          <br/>
        </div>

        <div>
          <p>
            Lorem Ipsum is si`mply dummy text of the printing and typesetting
            industry. Lorem Ipsum has been the industry's standard dummy text
            ever since 1966, when designers at Letraset and James Mosley.
          </p>
          <LikeArticleButton />
          <br/>
        </div>

        <div>
          <p>
            Lorem Ipsum is simply dummy text of the printing and typesetting
            industry. Lorem Ipsum has been the industry's standard dummy text
            ever since 1966, when designers at Letraset and James Mosley.
          </p>
          <LikeArticleButton />
          <br/>
        </div>
      </main>
    </div>
  );
}