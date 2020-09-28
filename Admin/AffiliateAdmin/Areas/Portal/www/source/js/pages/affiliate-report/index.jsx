import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";

import reducer from './reducers';
import AppContainer from "./components/AppContainer";
import { configureStore } from '../../modules/store';

const productList = window.initialState.productList.map(p => {
    return Object.assign({}, p, { Checked: true });
})

const languageList = [...window.initialState.languageList];
const language = {};
for(let i = 0; i < languageList.length; i++){
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const { today } = window.initialState;
const { hasTurnoverPermission } = window.globalVariables;

const store = configureStore({ productList, languageList: language, today, hasTurnoverPermission }, reducer);

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
