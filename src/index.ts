import App from "./app/main";

async function Main() {
    const app = new App;
    app.listen()
}
Main().catch(err => {
    console.error(`Error to start the server ${err}`)
    process.exit(1)
})