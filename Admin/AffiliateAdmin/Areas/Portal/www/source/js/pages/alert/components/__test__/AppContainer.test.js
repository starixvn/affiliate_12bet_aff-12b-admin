import React from 'react';
import AppContainer from '../AppContainer';
import { shallow } from 'enzyme';

import * as utils from '../../../../modules/utils';

describe('alert - components - AppContainer', () => {

    const defaultParam = {
            Code: 1,
            Link: 'UserControl/ListUser.aspx',
            User: 'sa'
        }

    it('render correctly', () => {
        const result = shallow(<AppContainer param={defaultParam} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with invalid code', () => {
        const param = Object.assign({}, defaultParam, { Code: 99 })
        const result = shallow(<AppContainer param={param} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call window.setTimeout', () => {
        const result = shallow(<AppContainer param={defaultParam} />);
        window.setTimeout = jest.fn((fn, val) => fn && fn());
        utils.resolveClientUrl = jest.fn();

        result.instance().componentDidMount();

        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(utils.resolveClientUrl).toHaveBeenCalledWith('UserControl/ListUser.aspx');
    });
});