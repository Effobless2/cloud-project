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
var apiUrl = 'https://localhost:44325/api/todo/'

async function getTasks(){
    let result = await axios.get(apiUrl, {
        header: {'Access-Control-Allow-Origin': '*'}
    })
    .then(function(response){
        return response.data;
    })
    .catch(function (error){
        console.log(error);
    });
    return result;
}

async function createTask(name, content){
    let result = await axios.put(apiUrl,
    {
        checked: false,
        name: name,
        content: content
    }, 
    {
        header: {
            "Access-Control-Allow-Origin": "*",
            "Content-type": "application/json"
        }
    })
    .then(function(response){
        return response.data;
    })
    .catch(function(error){
        console.log(error);
    })
    return result;
}

async function check(id){
    console.log("connard");
    let result = await axios.patch(apiUrl + `Check/${id}`, {
        header: {
            "Access-Control-Allow-Origin": "*"
        }
    })
    .then(function(response){
        return response.data;
    })
    .catch(function(error){
        console.log(error);
    })
    return result;
}

async function deleteTask(id){
    let result = axios.delete(apiUrl + id,{
        header: {
            "Access-Control-Allow-Origin": "*"
        }
    })
    .then(function(response){
        return response.data;
    })
    .catch(function(error){
        console.log(error);
    })
    return result;
}