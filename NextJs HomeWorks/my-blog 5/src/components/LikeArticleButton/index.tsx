"use client";

import { useState } from "react";

function LikeArticleButton() {
  const [likes, setLikes] = useState(0);
  return (
    <div>
      <button
        onClick={() => setLikes((prev) => prev + 1)}
        className="border border-gray-300 dark:border-gray-600 px-3 py-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700 dark:text-white transition-colors"
      >
        Поставить лайк:{likes}
      </button>
    </div>
  );
}

export default LikeArticleButton;
