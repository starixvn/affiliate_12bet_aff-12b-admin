import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

describe('specific domain control - components - Filter ', () => {
    const defaultProps = {
        setParam: jest.fn()
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);
        expect(result).toMatchSnapshot();
    });

    it('should update state when input values change', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        
        // Test affName input
        wrapper.find('Input[name="affName"]').simulate('change', { target: { value: 'testuser' } });
        expect(wrapper.find('Input[name="affName"]').props().value).toBe('testuser');

        // Test affDomain input
        wrapper.find('Input[name="affDomain"]').simulate('change', { target: { value: 'test.com' } });
        expect(wrapper.find('Input[name="affDomain"]').props().value).toBe('test.com');

        // Test redirectDomain input
        wrapper.find('Input[name="redirectDomain"]').simulate('change', { target: { value: 'redirect.com' } });
        expect(wrapper.find('Input[name="redirectDomain"]').props().value).toBe('redirect.com');
    });

    it('should call setParam with correct values when search button is clicked', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        
        // Set input values
        wrapper.find('Input[name="affName"]').simulate('change', { target: { value: 'testuser' } });
        wrapper.find('Input[name="affDomain"]').simulate('change', { target: { value: 'test.com' } });
        wrapper.find('Input[name="redirectDomain"]').simulate('change', { target: { value: 'redirect.com' } });

        // Click search button
        wrapper.find('Button').simulate('click');

        expect(defaultProps.setParam).toHaveBeenCalledWith({
            AffName: 'testuser',
            AffDomain: 'test.com',
            RedirectDomain: 'redirect.com'
        });
    });

    it('should call setParam when Enter key is pressed', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const preventDefault = jest.fn();
        
        // Set input values
        wrapper.find('Input[name="affName"]').simulate('change', { target: { value: 'testuser' } });
        wrapper.find('Input[name="affDomain"]').simulate('change', { target: { value: 'test.com' } });
        wrapper.find('Input[name="redirectDomain"]').simulate('change', { target: { value: 'redirect.com' } });

        // Simulate Enter key press on any input
        wrapper.find('Input[name="affName"]').simulate('keyPress', { charCode: 13, preventDefault });

        expect(preventDefault).toHaveBeenCalled();
        expect(defaultProps.setParam).toHaveBeenCalledWith({
            AffName: 'testuser',
            AffDomain: 'test.com',
            RedirectDomain: 'redirect.com'
        });
    });

    it('should not call setParam when non-Enter key is pressed', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        
        // Set input values
        wrapper.find('Input[name="affName"]').simulate('change', { target: { value: 'testuser' } });
        wrapper.find('Input[name="affDomain"]').simulate('change', { target: { value: 'test.com' } });
        wrapper.find('Input[name="redirectDomain"]').simulate('change', { target: { value: 'redirect.com' } });

        // Simulate non-Enter key press
        wrapper.find('Input[name="affName"]').simulate('keyPress', { charCode: 65 }); // 'A' key

        expect(defaultProps.setParam).toHaveBeenCalledWith({
            AffName: 'testuser',
            AffDomain: 'test.com', 
            RedirectDomain: 'redirect.com'
        });
    });

    it('should handle empty input values', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        
        // Click search button without setting any values
        wrapper.find('Button').simulate('click');

        expect(defaultProps.setParam).toHaveBeenCalledWith({
            AffName: '',
            AffDomain: '',
            RedirectDomain: ''
        });
    });
}); 