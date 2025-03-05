import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import AppContainer from './components/AppContainer';

import reducer from './reducers';
import { configureStore } from '../../../modules/store';

export default class App extends React.PureComponent {
    constructor(props) {
        super(props);

        const { marketList, affSubDomainList } = window.initialState;

        const newAffSubDomainList = affSubDomainList.map(item => {
            return {
                columnKey: item,
                columnName: item,
                checked: true
            }
        });

        this.store = configureStore({ marketList, affSubDomainList: newAffSubDomainList }, reducer);
    }

    render() {
        return (
            <Provider store={this.store}>
                <AppContainer />
            </ Provider>
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));