var tasklist = [
    {
        name: "Say Hello",
        content: "Put the coconut in a nut",
        id: 1,
        checked: false
    },
    {
        name: "Say Me",
        content: "Put   in a nut",
        id: 2,
        checked: true
    },
    {
        name: "Say Don't",
        content: "Put the  in a nut",
        id: 3,
        checked: false
    },
    {
        name: "Say Point",
        content: "Put the coconut in a ",
        id: 4,
        checked: true
    },
    {
        name: "Say Hello",
        content: "Put the coconut  a nut",
        id: 5,
        checked: true
    },
]

function getTasks(){
    return tasklist;
}

function createTask(name, content){
    let id = 0
    for(let i = 0; i < tasklist.length; i++)
        if(tasklist[i].id > id) id = tasklist[i].id;
    
    tasklist.push(
        {
            id:id+1,
            name: name,
            content: content,
            checked: false
        }
    );
    return id+1;
}

function check(id){
    for(let i = 0; i < tasklist.length; i++){
        if (tasklist[i].id == id){
            tasklist[i].checked = !tasklist[i].checked;
            break;
        }
    }
}

function deleteTask(id){
    for(let i = 0; i < tasklist.length; i++)
        if(tasklist[i].id == id){
            tasklist.splice(i,1);
            break;
        }
}