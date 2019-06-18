import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import { updateLanguage, getAffiliateInfo } from '../../actions';
import { messages } from '../../modules/constants';

export class Language extends PureComponent {
    constructor(props) {
        super(props);

        this.languageDropdown = React.createRef();

        this.state = {
            isChangingLanguage: false
        }
    }

    handleChangeLanguage = async (event) => {
        const isConfirmed = window.confirm(messages.areYouChangeLanguage);
        if (isConfirmed) {
            const preferredLanguage = event.target.value;
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, PreferredLanguage: preferredLanguage });
            const result = await this.props.updateLanguage(param);

            if (result.IsSuccessful) {
                window.alert(messages.languageUpdatedSuccess);
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(result.Message);
            }
        }
    }

    toggleChangingLanguage = (isChanging) => {
        this.setState({ isChangingLanguage: isChanging }, () => isChanging && this.languageDropdown.current.focus());
    }

    _renderLanguageList() {
        const { affiliateInfo, languageList } = this.props;

        return (
            <select value={affiliateInfo.PreferredLanguage}
                onChange={this.handleChangeLanguage} onBlur={() => this.toggleChangingLanguage(false)} ref={this.languageDropdown}>
                {
                    languageList.map(language => {
                        return (<option value={language.LanguageCode} key={`affinfo-lang-${language.LanguageCode}`}>{language.LanguageName}</option>)
                    })
                }
            </select>
        )
    }

    _renderLanguageLabel() {
        const { affiliateInfo, languageList } = this.props;
        const languageItem = languageList.find(l => l.LanguageCode === affiliateInfo.PreferredLanguage);

        return (
            <Button color="link" className="left-panel-btn-link" onClick={() => this.toggleChangingLanguage(true)}>
                {languageItem && languageItem.LanguageName}
            </Button>
        )
    }

    render() {
        const { isChangingLanguage } = this.state;

        return isChangingLanguage ? this._renderLanguageList() : this._renderLanguageLabel();
    }
}

function mapStateToProps({ affCommonInfo, leftPanel, languageList }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, languageList, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateLanguage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Language);