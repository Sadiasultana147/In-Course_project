const fs = require('fs-extra');
const express = require('express');
const formidable = require('formidable');
const app = express();
const repo = require('./lib/repository').repo;

app.set('view engine', "raz");

app.use(express.static(__dirname + '/www'));
console.log(repo.data);
app.get('/list', (req, res) => {
    res.json(repo.data);
});
app.post("/create", (req, res) => {
    var form = new formidable.IncomingForm();
    var book = {};
	book.id = repo.data.length+1;
    form.parse(req, (err, flds, files) => {
        //console.log(flds);
		book.bookName = flds.book_name;
        book.writer = flds.writer;
        book.category = flds.category;
        //console.log(files.picture.path);
        book.picture = 'uploads/' + files.picture.name;
        repo.insert(book);
        var src = files.picture.path;
        var dest = __dirname + "\\www\\Images\\uploads\\" + files.picture.name
        
        fs.copy(src, dest, function (err) {
            if (err) {
                console.error(err);
            } else {
                console.log("success!")
            }
        });
        res.redirect("/book.html");
    });
});

app.get('/edit/:id', (req, res)=>{
  var book =repo.get(req.params.id);
  console.log(book);
  res.render('edit', {id:book.id, 
             bookName:book.bookName,
             writer: book.writer,
             category:book.category,			 
             picture: book.picture});
});

app.post('/edit/:id', (req, res)=>{
  var id = req.params.id;
  var form = new formidable.IncomingForm();
  form.parse(req, (err, flds)=>{
    repo.update(id, {bookName:flds.book_name,writer:flds.writer,  
        category:flds.category});
  });
  res.redirect("/book.html");
  
});

app.listen(8181);
console.log("http://localhost:8181 running..");