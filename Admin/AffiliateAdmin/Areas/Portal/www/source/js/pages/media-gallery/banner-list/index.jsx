import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const categoryList = [...window.initialState.categoryList];
const category = {};
for (let i = 0; i < categoryList.length; i++) {
    const categoryItem = categoryList[i];
    category[categoryItem.Name] = categoryItem.Name;
}

const languageList = [...window.initialState.languageList];
const language = {};
for (let i = 0; i < languageList.length; i++) {
    const languageItem = languageList[i];
    language[languageItem.LanguageCode] = languageItem.LanguageName;
}

const currencyList = [...window.initialState.currencyList];
const currency = {};
for (let i = 0; i < currencyList.length; i++) {
    const currencyItem = currencyList[i];
    currency[currencyItem.Name] = currencyItem.Name;
}

const sizeList = [...window.initialState.sizeList];
const size = {};
for (let i = 0; i < sizeList.length; i++) {
    const sizeItem = sizeList[i];
    size[sizeItem.Name] = sizeItem.Name;
}

const store = configureStore({ categoryList: category, languageList: language, currencyList: currency, sizeList: size }, reducer);

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Provider store={store}>
                <AppContainer />
            </ Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));