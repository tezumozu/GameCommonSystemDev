using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCommonSystem_V6{

    public interface ICoroutineOrderable {

        /// <summary>
        /// コルーチンを受け取って開始する、
        /// すでに開始したコルーチンを受け取った場合何もしない、
        /// 停止しているコルーチンを受け取った場合再開する
        /// </summary>
        /// <param name="coroutine">開始するコルーチン</param>
        public void OrderStartCoroutine(IEnumerator coroutine);

        /// <summary>
        /// コルーチンのリストを受け取って全てのコルーチンを登録し開始する、
        /// すでに開始したコルーチンを受け取った場合何もしない、
        /// 停止しているコルーチンを受け取った場合再開する
        /// </summary>
        /// <param name="coroutineList">開始するコルーチンのリスト</param>
        public void OrderStartCoroutine(List<IEnumerator> coroutineList);



        /// <summary>
        /// コルーチンを停止する、
        /// すでにコルーチンが停止していたら何もしない、
        /// コルーチンが登録されていなければ何もしない
        /// </summary>
        /// <param name="coroutine">停止するコルーチン</param>
        public void OrderStopCoroutine(IEnumerator coroutine);

        /// <summary>
        /// コルーチンのリストを受け取り、すべてのコルーチンを停止する、
        /// コルーチンがすでに停止していたら何もしない、
        /// 登録されていないコルーチンに対しては何もしない
        /// </summary>
        /// <param name="coroutineList">停止するコルーチンのリスト</param>
        public void OrderStopCoroutine(List<IEnumerator> coroutineList);



        /// <summary>
        /// 受け取ったコルーチンを完全に停止する
        /// </summary>
        /// <param name="coroutine">破棄するコルーチン</param>
        public void OrderKillCoroutine(IEnumerator coroutine);

        /// <summary>
        /// コルーチンのリストを受け取り、受け取ったコルーチンを破棄する、
        /// 登録されていないコルーチンに対しては何もしない
        /// </summary>
        /// <param name="coroutineList">破棄するコルーチンのリスト</param>
        public void OrderKillCoroutine(List<IEnumerator> coroutineList);



        /// <summary>
        /// 受け取ったコルーチンが登録されているか
        /// ＝コルーチンが終了しているか成否を返す。
        /// </summary>
        /// <param name="coroutine">対象のコルーチン</param>
        /// <returns> 
        /// true：登録されている＝終了していない
        /// false：登録されていない＝コルーチンの終了
        /// </returns>
        public bool IsRegistrationCoroutine(IEnumerator coroutine);

    }

}
