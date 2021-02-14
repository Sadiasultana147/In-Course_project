function Repo() {
    this.data= [
        { id:'1', bookName:'The Hobbit', writer:'J.R.R Tolkien', category:'Action & Adventure', picture:'The Hobbit.jpg'  },
        { id:'2',bookName:'The Poets Laureate Anthology', writer:'Alexander Dumas', category:'Action & Adventure' ,picture:'The Poets Laureate Anthology.jpg'  },
        { id:'3',bookName:'The Three Musketeers',writer:'Elizabeth Hun Schmidt',category:'Anthology', picture:'The Three Musketeers.jpg' }
    ];
    this.insert = (book) => {
        this.data.push(book);
    
    }
    this.get = (id)=>{
        var book = this.data.filter(p=> p.id == id);
        if(book && book.length) return book[0];
        else return null;
    }
    this.update = (id, book) => {
        var books= this.data.filter(b=> b.id == id);
        if(books || books.length)
        {
          
          books[0].bookName = book.bookName;
          books[0].writer = book.writer;
		  books[0].category=book.category;
          
          console.log(this.books);
        }
    };
}
module.exports.repo = new Repo();