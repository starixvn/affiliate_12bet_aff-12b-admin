import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

const { categoryList, subCategoryList, languageList, currencyList, imageTypeList, sizeList, affTypeList, paramFromHome } = window.initialState;

const newCategoryList = categoryList.map(item => {
    return {
        columnKey: item.CategoryId,
        columnName: item.CategoryName,
        checked: false
    };
});

const newSubCategoryList = subCategoryList.map(item => {
    return {
        columnKey: item.CategoryId,
        columnName: item.CategoryName,
        parentCategoryId: item.ParentCategoryId,
        checked: false
    };
});

const newCurrencyList = currencyList.map(item => {
    return {
        columnKey: item.Id,
        columnName: item.Name,
        checked: false
    };
});

const newImageTypeList = imageTypeList.map(item => {
    return {
        columnKey: item.Id,
        columnName: item.Name,
        checked: false
    };
});

const newSizeList = sizeList.map(item => {
    return {
        columnKey: item.Id,
        columnName: item.Name,
        checked: false
    };
});

const newAffTypeList = affTypeList.map(item => {
    return {
        columnKey: item.AffTypeId,
        columnName: item.AffTypeName,
        checked: false
    };
});

const store = configureStore({
    languageList, paramFromHome, categoryList: newCategoryList, subCategoryList: newSubCategoryList,
    currencyList: newCurrencyList, imageTypeList: newImageTypeList, sizeList: newSizeList, affTypeList: newAffTypeList
}, reducer);

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