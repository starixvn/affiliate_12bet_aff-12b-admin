import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createStore, applyMiddleware, compose } from "redux";
import thunkMiddleware from "redux-thunk";

import reducer from './reducers';
import AppContainer from "./components/AppContainer";

function configureStore(initialState) {
    const enhancer = compose(
        applyMiddleware(
            thunkMiddleware // lets us dispatch() functions
        )
    );

    return createStore(reducer, initialState, enhancer);
}

const productList = window.initialState.productList.map(p => {
    return Object.assign({}, p, { Checked: true });
})

const languageList = [...window.initialState.languageList];
const language = {};
for(let i = 0; i < languageList.length; i++){
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const today = window.initialState.today;

const store = configureStore({ productList, languageList: language, today });

export default class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));
