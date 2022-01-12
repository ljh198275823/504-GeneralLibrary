using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Speech
{
    public class TTSSpeech
    {
        #region 静态方法和属性
        private static TTSSpeech _Instance;
        public static TTSSpeech Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TTSSpeech();
                }
                return _Instance;
            }
        }
        #endregion

        private SpeechLib.SpVoice _Voice;

        #region 构造方法
        private  TTSSpeech()
        {
            _Voice = new SpeechLib.SpVoice();
        }
        #endregion

        #region 公共方法
        public void Speek(string msg,bool asyc=true)
        {
            try
            {
                _Voice.Skip("Sentence", int.MaxValue);
                if (asyc)
                {
                    _Voice.Speak(msg, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
                }
                else
                {
                    _Voice.Speak(msg, SpeechLib.SpeechVoiceSpeakFlags.SVSFDefault);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }

        public void Pause()
        {
            _Voice.Pause();
        }

        public void Resume()
        {
            _Voice.Resume();
        }

        public void Skip(int num)
        {
            _Voice.Skip("Sentence", num);
        }
        #endregion
    }
}
