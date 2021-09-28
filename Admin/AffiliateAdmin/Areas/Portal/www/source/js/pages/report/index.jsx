import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";

import reducer from './reducers';
import AppContainer from "./components/AppContainer";
import { configureStore } from '../../modules/store';

import ClaimPrincipal from '../../modules/claim';

export default class App extends React.Component {
    constructor(props) {
        super(props);

        const productList = window.initialState.productList.map(p => {
            return Object.assign({}, p, { Checked: true });
        })

        const languageList = [...window.initialState.languageList];
        const language = {};
        for (let languageItem of languageList) {
            language[languageItem.LanguageCode] = languageItem.LanguageName;
        }

        const { today } = window.initialState;

        this.store = configureStore({ productList, languageList: language, today, claimsPrincipal: ClaimPrincipal.currentClaimPrincipal }, reducer);
    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));
