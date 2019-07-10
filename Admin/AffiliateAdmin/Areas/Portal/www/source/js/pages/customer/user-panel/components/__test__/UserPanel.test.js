import React from 'react';
import { UserPanel } from '../UserPanel';
import { shallow } from 'enzyme';

describe('customer - user-panel - components - UserPanel', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });
    const defaultProps = {
        selectedUserPanel: 'statementHistory',
        isShowLoader: true,
        onClickMenu: jest.fn(),
        isPending: false
    }

    it('Should render correctly with is pending', () => {
        const wrapper = shallow(<UserPanel {...defaultProps}/>);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with not pending', () => {
        const props = Object.assign({}, defaultProps, { isPending: true });
        const wrapper = shallow(<UserPanel {...props}/>);

        expect(wrapper).toMatchSnapshot();
    });
})