import React from 'react';
import { UserPanelMenuItem } from '../UserPanelMenuItem';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';

describe('customer - user-panel - components - UserPanelMenuItem', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });
    const defaultProps = {
        isActive: true,
        item: {
            key: "edit",
            name: "Edit",
            parent: null
        },
        onClickMenu: jest.fn()
    }

    it('Should render correctly with is active', () => {
        const wrapper = shallow(<UserPanelMenuItem {...defaultProps}/>);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with not active', () => {
        const props = Object.assign({}, defaultProps, { isActive: false });
        const wrapper = shallow(<UserPanelMenuItem {...props}/>);

        expect(wrapper).toMatchSnapshot();
    });

    it('onClickMenu - should be called when click menu', () => {
        const wrapper = shallow(<UserPanelMenuItem {...defaultProps}/>);
        wrapper.find(MyLink).simulate('click');

        expect(defaultProps.onClickMenu).toHaveBeenCalledWith('edit');
    });
})