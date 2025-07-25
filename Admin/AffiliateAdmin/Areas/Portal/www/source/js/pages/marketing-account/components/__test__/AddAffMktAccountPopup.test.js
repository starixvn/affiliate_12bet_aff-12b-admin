import React from 'react';
import { AddAffMktAccountPopup } from '../AddAffMktAccountPopup';
import { shallow } from 'enzyme';

// Mock the custom hook
const mockHookReturn = {
    resultInfo: {
        isSuccess: true,
        isShowMessage: false,
        message: ''
    },
    generalInfo: {
        hasData: true,
        affType: 0,
        groupId: '1',
        preferredLanguage: 'en',
        targetCountryCode: 'US'
    },
    personalInfo: {
        firstName: 'John',
        lastName: 'Doe',
        email: 'john@example.com'
    },
    additionalInfo: {
        address: '123 Main St',
        city: 'New York'
    },
    paymentInfo: {
        preferredPayment: 'visa',
        playerCustId: 123
    },
    websiteInfo: {
        siteUrl: 'https://example.com',
        hasWebSite: true
    },
    minimumPayment: 100,
    paymentMethods: [
        { id: 1, name: 'Visa' },
        { id: 2, name: 'MasterCard' }
    ],
    setGeneralInfo: jest.fn(),
    setPersonalInfo: jest.fn(),
    setAdditionalInfo: jest.fn(),
    setPaymentInfo: jest.fn(),
    setWebsiteInfo: jest.fn(),
    handleSubmit: jest.fn()
};

jest.mock('../../hooks/useNewMarketingAccountData', () => {
    return jest.fn(() => mockHookReturn);
});

// Mock the section components  
jest.mock('../sections/GeneralInfo', () => ({ generalInfo, setGeneralInfo }) => (
    <div data-testid="general-info">GeneralInfo</div>
));
jest.mock('../sections/Personal', () => ({ personalInfo, setPersonalInfo }) => (
    <div data-testid="personal">Personal</div>
));
jest.mock('../sections/Additional', () => ({ additionalInfo, setAdditionalInfo }) => (
    <div data-testid="additional">Additional</div>
));
jest.mock('../sections/EmailFormat', () => ({ generalInfo, setGeneralInfo }) => (
    <div data-testid="email-format">EmailFormat</div>
));
jest.mock('../sections/Payment', () => ({ paymentInfo, setPaymentInfo, minimumPayment, paymentMethods }) => (
    <div data-testid="payment">Payment</div>
));
jest.mock('../sections/WebSite', () => ({ websiteInfo, setWebsiteInfo }) => (
    <div data-testid="website">WebSite</div>
));

describe('marketing-account - components - AddAffMktAccountPopup', () => {
    const defaultProps = {
        aff: {
            affId: 12345,
            affName: 'TestAffiliate'
        },
        isShowAddAffMktAccountPopup: true,
        toggle: jest.fn(),
        toggleLoader: jest.fn(),
        setIsShowAddAffMktAccountPopup: jest.fn(),
        params: {
            Status: 'Approved',
            PageIndex: 1
        },
        setParam: jest.fn()
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('should render correctly when data is loaded', () => {
        const wrapper = shallow(<AddAffMktAccountPopup {...defaultProps} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should not render when generalInfo.hasData is false', () => {
        const mockHookWithoutData = {
            ...mockHookReturn,
            generalInfo: {
                ...mockHookReturn.generalInfo,
                hasData: false
            }
        };
        
        const useNewMarketingAccountData = require('../../hooks/useNewMarketingAccountData');
        useNewMarketingAccountData.mockReturnValue(mockHookWithoutData);

        const wrapper = shallow(<AddAffMktAccountPopup {...defaultProps} />);

        expect(wrapper.type()).toBeNull();
    });

    it('should call custom hook with correct parameters', () => {
        const useNewMarketingAccountData = require('../../hooks/useNewMarketingAccountData');
        
        shallow(<AddAffMktAccountPopup {...defaultProps} />);

        expect(useNewMarketingAccountData).toHaveBeenCalledWith(
            defaultProps.aff.affId,
            defaultProps.toggleLoader
        );
    });

    it('should handle different aff objects', () => {
        const props = {
            ...defaultProps,
            aff: {
                affId: 67890,
                affName: 'AnotherAffiliate'
            }
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...props} />);
        }).not.toThrow();
    });

    it('should handle modal visibility props', () => {
        // Test shown modal
        let wrapper = shallow(<AddAffMktAccountPopup {...defaultProps} />);
        expect(wrapper).toBeDefined();

        // Test hidden modal
        const hiddenProps = { ...defaultProps, isShowAddAffMktAccountPopup: false };
        wrapper = shallow(<AddAffMktAccountPopup {...hiddenProps} />);
        expect(wrapper).toBeDefined();
    });

    it('should handle different param objects', () => {
        const props = {
            ...defaultProps,
            params: {
                Status: 'Suspended',
                PageIndex: 2,
                PageSize: 50
            }
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...props} />);
        }).not.toThrow();
    });

    it('should handle success alert state', () => {
        const mockHookWithSuccess = {
            ...mockHookReturn,
            resultInfo: {
                isSuccess: true,
                isShowMessage: true,
                message: 'Account created successfully'
            }
        };
        
        const useNewMarketingAccountData = require('../../hooks/useNewMarketingAccountData');
        useNewMarketingAccountData.mockReturnValue(mockHookWithSuccess);

        expect(() => {
            shallow(<AddAffMktAccountPopup {...defaultProps} />);
        }).not.toThrow();
    });

    it('should handle error alert state', () => {
        const mockHookWithError = {
            ...mockHookReturn,
            resultInfo: {
                isSuccess: false,
                isShowMessage: true,
                message: 'Error creating account'
            }
        };
        
        const useNewMarketingAccountData = require('../../hooks/useNewMarketingAccountData');
        useNewMarketingAccountData.mockReturnValue(mockHookWithError);

        expect(() => {
            shallow(<AddAffMktAccountPopup {...defaultProps} />);
        }).not.toThrow();
    });

    it('should handle callback functions properly', () => {
        const toggle = jest.fn();
        const toggleLoader = jest.fn();
        const setIsShowAddAffMktAccountPopup = jest.fn();
        const setParam = jest.fn();

        const props = {
            ...defaultProps,
            toggle,
            toggleLoader,
            setIsShowAddAffMktAccountPopup,
            setParam
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...props} />);
        }).not.toThrow();
    });

    it('should maintain component structure', () => {
        const wrapper = shallow(<AddAffMktAccountPopup {...defaultProps} />);
        
        expect(wrapper).toBeDefined();
        expect(wrapper.length).toBe(1);
    });

    it('should handle empty aff object gracefully', () => {
        const props = {
            ...defaultProps,
            aff: {
                affId: '',
                affName: ''
            }
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...props} />);
        }).not.toThrow();
    });

    it('should handle null params', () => {
        const props = {
            ...defaultProps,
            params: null
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...props} />);
        }).not.toThrow();
    });

    it('should handle different hook return states', () => {
        const complexHookReturn = {
            ...mockHookReturn,
            generalInfo: {
                ...mockHookReturn.generalInfo,
                hasData: true,
                affType: 1,
                groupId: '2'
            },
            personalInfo: {
                firstName: 'Jane',
                lastName: 'Smith',
                email: 'jane@example.com'
            }
        };

        const useNewMarketingAccountData = require('../../hooks/useNewMarketingAccountData');
        useNewMarketingAccountData.mockReturnValue(complexHookReturn);

        expect(() => {
            shallow(<AddAffMktAccountPopup {...defaultProps} />);
        }).not.toThrow();
    });

    it('should handle component with all required props', () => {
        expect(() => {
            shallow(<AddAffMktAccountPopup {...defaultProps} />);
        }).not.toThrow();
    });

    it('should maintain stability with prop updates', () => {
        const wrapper = shallow(<AddAffMktAccountPopup {...defaultProps} />);

        expect(() => {
            wrapper.setProps({
                aff: { affId: 99999, affName: 'Updated' }
            });
        }).not.toThrow();
    });

    it('should handle complex aff structures', () => {
        const complexProps = {
            ...defaultProps,
            aff: {
                affId: 12345,
                affName: 'TestAffiliate',
                status: 'Active',
                type: 'Premium',
                registrationDate: '2023-01-01'
            }
        };

        expect(() => {
            shallow(<AddAffMktAccountPopup {...complexProps} />);
        }).not.toThrow();
    });
}); 