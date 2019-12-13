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