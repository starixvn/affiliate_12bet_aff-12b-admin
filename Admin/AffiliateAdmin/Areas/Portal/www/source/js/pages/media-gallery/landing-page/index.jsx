import React from 'react';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';
import { convertArrayToObjectProps } from '../../../modules/utils';
export default class App extends React.PureComponent {
    constructor(props) {
        super(props);

        const { targetCountryList } = window.initialState;
        const newTargetCountryList = convertArrayToObjectProps(targetCountryList, 'TargetCountryCode', 'TargetCountryName');

        this.store = configureStore({
            targetCountryList: newTargetCountryList
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