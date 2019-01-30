console.log("Port");

function buyGoodById(good){
    console.log(good);
    let welcome = document.getElementById("tradeWelcome");
    let buy = document.getElementById("buyForm");
    console.log(welcome);
    welcome.style.display="none";
    let price = 34;
    let goodId=123;
    buy.innerHTML=`
    <h4>Buy ${good}</h4>
    <form action="/buy" method="POST">
    <p>Quantity:</p>
    <input type="number" name="quantity">
    

    <button>Buy</button>
    </form>`;
    buy.style.display="block";
}