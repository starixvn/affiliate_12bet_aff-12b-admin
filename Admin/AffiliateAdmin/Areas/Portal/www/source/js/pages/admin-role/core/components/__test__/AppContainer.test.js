import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('admin role - core - components - AppContainer', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} selectedMenu={'adminList'} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not menu item', () => {
        const result = shallow(<AppContainer isShowLoader={true} selectedMenu={'Abc'} />);

        expect(result).toMatchSnapshot();
    });
});