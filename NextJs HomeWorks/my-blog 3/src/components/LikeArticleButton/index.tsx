"use client";

import { useState } from "react";

function LikeArticleButton() {
  const [likes, setLikes] = useState(0);
  return (
    <div>
      <button
        onClick={() => setLikes((prev) => prev + 1)}
        style={{ borderWidth: 1 }}
      >
        Поставить лайк:{likes}
      </button>
    </div>
  );
}

export default LikeArticleButton;
