import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import ClaimPrincipal from '../../../modules/claim';
import { rearrangeItems } from './libs/utils';

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);

        const { targetProductList, languageList, sizeList, affTypeList,
            paramFromHome, bannerRootUrl, today } = window.initialState;

        const newTargetProductList = targetProductList.map(item => {
            return {
                key: item.TargetProductId,
                name: item.TargetProductName,
                checked: false
            };
        });

        const newSizeList = rearrangeItems(sizeList);

        const newAffTypeList = affTypeList.map(item => {
            return {
                key: item.AffTypeId,
                name: item.AffTypeName,
                checked: false
            };
        });

        const claimsPrincipal = ClaimPrincipal.currentClaimPrincipal;
        this.store = configureStore({
            languageList, paramFromHome, targetProductList: newTargetProductList, sizeList: newSizeList, affTypeList: newAffTypeList,
            bannerRootUrl, claimsPrincipal, today
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
