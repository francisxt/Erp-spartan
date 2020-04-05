

const GetAllClientOptions = async () => {
    await fetch("/ClientUser/GetAllOptionsClients").then(response => response.text()).then((result) => { console.log(result); });
};