import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('admin list - components - AppContainer', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} hasWriteAdminRoleClaim={true} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not permission', () => {
        const result = shallow(<AppContainer isShowLoader={true} hasWriteAdminRoleClaim={false} />);

        expect(result).toMatchSnapshot();
    });
});