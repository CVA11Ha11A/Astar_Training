using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;

public static partial class GFunc
{
    [System.Diagnostics.Conditional("DEBUG_MODE")]


    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif
    }


    //아래의 코드는 위에서 래핑한 Log 코드와 동일하지만 뒤에 Object context매개변수로
    //글자의 색을 바꿀수 있다
    public static void Log001(object message, Object context)
    {
        Debug.Log(message, context);
    }

    [System.Diagnostics.Conditional("DEBUG_MOD")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }




    //! GameObject 받아서 Text 컴포넌트 찾아서 text 필드 값 수정하는 함수
    public static void SetText(this GameObject target, string text)
    {
        Text textcomponent = target.GetComponent<Text>();
        if (textcomponent == null || textcomponent == default) { return; }

        textcomponent.text = text;
    }


    //! LoadScene 함수 래핑한다.
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //!두 백터를 더한다.
    public static Vector2 AddVector(this Vector3 origin, Vector2 addVector)
    {
        
        Vector2 result = new Vector2(origin.x, origin.y);
        result += addVector;
        return result;
    }


    //! 현재씬의 이름을 리턴한다
    public static string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //! 컴포넌트가 존재하는지 여부를 체크하는 함수         제한자  = :
    public static bool IsValid<T>(this T target) where T : Component
    {
        if (target == null || target == default) { return false; }
        else { return true; }
    }

    //! 리스트가 유효한지 여부를 체크하는 함수            제한자  = :
    public static bool IsValid<T>(this List<T> target)  //where T : Component //앞에 주석을 풀면 제한자를 Component로 사용가능
    {
        bool isInvalid = (target == null || target == default);
        isInvalid = isInvalid || target.Count == 0; 

        if (isInvalid == true) { return false; }
        else { return true; }
    }







    //맨위 부모 오브젝트 = 루프 오브젝트
    //루트 오브젝트를 가지고 오는 코드
    //! 활성화 된 현재 씬의 루트 오브젝트를 서치해서 찾아주는 함수
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj_ in rootObjs_)
        {

            if (rootObj_.name.Equals(objName_))
            {
                targetObj_ = rootObj_;
                return targetObj_;

            }
            else { continue; }

        }
        return targetObj_;

    }   //GetRootObj






 //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        for(int i =0; i < targetObj_.transform.childCount; i++)
        {

            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if(searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }   //if : 내가 찾고 싶은 오브젝트를 찾은 경우

            else
            {
                searchResult = FindChildObj(searchTarget, objName_);

                
                    if (searchResult == null || searchResult == default) {/*Pass*/ }
                    else { return searchResult; }
                
            }   //else : 내가 찾고 싶은 오브젝트를 아직 못찾은 경우

        }   //loop : 탐색 타겟 오브젝트의 자식 오브젝트 갯수만큼 순회하는 루프

        return searchResult;
    }   //FindChildObj()









 //! 특정 오브젝트의 자식 오브젝트를 서치해서 컴포넌트를 찾아주는 함수
    public static T FindChildComponent<T>(
        this GameObject targetObj_, string objName_) where T : Component
    {

        T searchResultComponent = default(T);
        GameObject searchResultObj = default(GameObject);

        searchResultObj = targetObj_.FindChildObj(objName_);

        if(searchResultObj != null || searchResultObj != default)
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }

        return searchResultComponent;

    }       //FindChildComponent()

 

}
