import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';

const { categoryList, subCategoryList, languageList, currencyList, imageTypeList, sizeList, affTypeList,
    paramFromHome, subCategoryCasinoList, subCategorySportsbookList, bannerRootUrl } = window.initialState;

const newCategoryList = categoryList.map(item => {
    return {
        categoryName: item.CategoryName,
        key: item.CategoryName,
        name: item.CategoryName,
        checked: false
    };
});

const newSubCategoryList = subCategoryList.map(item => {
    return {
        key: item.SubCategoryName,
        name: item.SubCategoryName,
        parentCategoryName: item.CategoryName,
        checked: false
    };
});

const newCurrencyList = currencyList.map(item => {
    return {
        key: item.Name,
        name: item.Name,
        checked: false
    };
});

const newSizeList = sizeList.map(item => {
    return {
        key: item.Name,
        name: item.Name,
        checked: false
    };
});

const newAffTypeList = affTypeList.map(item => {
    return {
        key: item.AffTypeId,
        name: item.AffTypeName,
        checked: false
    };
});

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        const store = configureStore({
            languageList, paramFromHome, categoryList: newCategoryList, subCategoryList: newSubCategoryList,
            currencyList: newCurrencyList, imageTypeList, sizeList: newSizeList, affTypeList: newAffTypeList,
            subCategoryCasinoList, subCategorySportsbookList, bannerRootUrl, claimsPrincipal
        }, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </ Provider>
        );
    }
}
