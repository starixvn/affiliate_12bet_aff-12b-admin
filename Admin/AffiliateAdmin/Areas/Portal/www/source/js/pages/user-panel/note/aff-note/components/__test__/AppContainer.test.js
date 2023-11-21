import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import UserPanelSubMenu from '../../../../../common/components/UserPanelSubMenu';


describe('note - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer />);

        expect(result).toMatchSnapshot();
    });

    it('setSelectedUserPanel - should be called with note', () => {
        const props = Object.assign({}, { setSelectedUserPanel: jest.fn() });
        const wrapper = shallow(<AppContainer {...props} />);

        wrapper.find(UserPanelSubMenu).props().subMenuList[0].handleClick();

        expect(props.setSelectedUserPanel).toHaveBeenCalledWith('note');
    });

    it('setSelectedUserPanel - should be called with marketingNote', () => {
        const props = Object.assign({}, { setSelectedUserPanel: jest.fn() });
        const wrapper = shallow(<AppContainer {...props} />);

        wrapper.find(UserPanelSubMenu).props().subMenuList[1].handleClick();

        expect(props.setSelectedUserPanel).toHaveBeenCalledWith('marketingNote');
    });
});