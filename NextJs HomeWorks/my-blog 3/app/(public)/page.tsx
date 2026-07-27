import LikeArticleButton from "@/components/LikeArticleButton";
import Link from "next/link";
import styles from "../page.module.css"


export default function Home() {
  return (
    <main className={styles.container}>

      <h1 className={styles.title}>
        Home Task
      </h1>


      <div className={styles.article}>
        <Link 
          className={styles.link}
          href="/blog/1"
        >
          Статья №1
        </Link>

        <LikeArticleButton />
      </div>


      <div className={styles.article}>
        <Link
          className={styles.link}
          href="/blog/2"
        >
          Статья №2
        </Link>

        <LikeArticleButton />
      </div>


      <div className={styles.article}>
        <Link
          className={styles.link}
          href="/blog/3"
        >
          Статья №3
        </Link>

        <LikeArticleButton />
      </div>


    </main>
  );
}