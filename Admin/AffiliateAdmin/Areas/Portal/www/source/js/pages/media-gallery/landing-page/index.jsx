import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertListToMutilpleSelected } from '../../../modules/utils';

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);
        const { dealList } = window.initialState;
        const newDealList = convertListToMutilpleSelected(dealList, 'GroupId', 'GroupName', false);

        this.store = configureStore({ dealList: newDealList }, reducer);
    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </ Provider>
        );
    }
}