let task = [
  {
    id: 1,
    taskName: "Ajouter ",
    taskdate: "10/02/2019",
   
  },
    {
    id: 2,
    taskName: "Supprimer ",
    taskdate: "10/02/2019",
   
  },
    {
    id: 3,
    taskName: "Modifier ",
    taskdate: "10/02/2019",
   
  }
  
];
export let findAll = () => new Promise(resolve => resolve(task));