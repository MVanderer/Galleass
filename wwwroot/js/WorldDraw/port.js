console.log("Port");

function buyGoodById(portId,goodId,goodName){
    console.log(goodName);
    let welcome = document.getElementById("tradeWelcome");
    let buy = document.getElementById("buyForm");
    console.log(welcome);
    welcome.style.display="none";
    let price = 34;
    buy.innerHTML=`
    <h4>Buy ${goodName}</h4>
    <form action="/buy/${goodId}/${portId}" method="POST">
    <p>Quantity:</p>
    <input type="number" name="quantity">
    

    <button>Buy</button>
    </form>`;
    buy.style.display="block";
}

function sellGoodsById(goodId,goodName){

}