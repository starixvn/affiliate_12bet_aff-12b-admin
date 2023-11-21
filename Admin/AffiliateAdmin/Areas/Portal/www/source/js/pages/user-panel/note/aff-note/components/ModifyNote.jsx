import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { setShowPopup, getNoteList, insertNote, updateNote } from '../actions';
import InsertUpdateForm from '../../common/components/InsertUpdateForm';

const mapStateToProps = ({ affCommonInfo, note }) => {
    const { pagingOption, noteInfo, isShowPopup } = note;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        affId,
        isShowPopup,
        pagingOption,
        noteInfo
    });
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setShowPopup,
        getNoteList,
        insertNote,
        updateNote
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(InsertUpdateForm);