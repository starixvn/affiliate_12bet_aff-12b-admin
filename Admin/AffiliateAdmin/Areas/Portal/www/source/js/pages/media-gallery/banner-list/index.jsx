import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';
import { rearrangeCurrencyAndSizeItems } from './libs/utils';

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);

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

        const newSizeList = rearrangeCurrencyAndSizeItems(sizeList);
        const newCurrencyList = rearrangeCurrencyAndSizeItems(currencyList);

        const newAffTypeList = affTypeList.map(item => {
            return {
                key: item.AffTypeId,
                name: item.AffTypeName,
                checked: false
            };
        });

        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        this.store = configureStore({
            languageList, paramFromHome, categoryList: newCategoryList, subCategoryList: newSubCategoryList,
            currencyList: newCurrencyList, imageTypeList, sizeList: newSizeList, affTypeList: newAffTypeList,
            subCategoryCasinoList, subCategorySportsbookList, bannerRootUrl, claimsPrincipal
        }, reducer);
    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </ Provider>
        );
    }
}
