import { LikeButton } from "./LikedButton";

function DemoMixedPage() {
  const articleTitle = "Как работает рендеринг Next.js";
  return (
    <div>
      <h1>{articleTitle}</h1>
      <p>Этот статичный текст, отрендерен на сервере</p>
      <LikeButton/>
    </div>
  );
}

export default DemoMixedPage;
