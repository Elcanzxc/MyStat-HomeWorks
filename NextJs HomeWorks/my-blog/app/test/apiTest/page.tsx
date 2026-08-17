import { GetNotes } from "./action";
import { Note } from "./api/route";
import CreateNoteForm from "./createNoteForm";
import DeleteNoteForm from "./deleteNoteForm";
import UpdateNoteForm from "./updateNoteForm";

export default async function Page() {
  const notes = await GetNotes();

  return (
    <div>
      <div>
        {notes.map((item: Note) => (
          <div key={item.id}>
            <p >
              {item.id}: {item.text}
            </p>
            <div >
              <UpdateNoteForm id={item.id} />
              <DeleteNoteForm id={item.id} />
            </div>
          </div>
        ))}
      </div>

      <div>
        <CreateNoteForm />
      </div>
    </div>
  );
}
