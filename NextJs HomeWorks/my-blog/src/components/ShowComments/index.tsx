

import Comments from "../Comments";
import { comments } from "../Comments/comments-store";




export default function ShowComments() {

  return (
    <div className="mt-12 pt-8 border-t border-slate-200 dark:border-slate-800">
      <h2 className="text-2xl font-bold text-slate-800 dark:text-slate-100 mb-6">Комментарии</h2>
      
      <div className="mb-8">
        <Comments/>
      </div>

      <div className="space-y-4">
        {comments.length === 0 ? (
          <p className="text-slate-500 dark:text-slate-400 italic bg-slate-50 dark:bg-slate-800/50 p-4 rounded-lg">
            Комментариев пока нет. Будьте первым!
          </p>
        ) : (
          comments.map((comment, index) => (
            <div key={index} className="bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 p-5 rounded-xl shadow-sm transition-all hover:shadow-md">
                <strong className="block text-slate-900 dark:text-white font-semibold mb-2">
                  {comment.author}
                </strong>
                <p className="text-slate-700 dark:text-slate-300 leading-relaxed">
                  {comment.text}
                </p>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
